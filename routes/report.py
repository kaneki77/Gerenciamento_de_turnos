from flask import Blueprint, request, jsonify, make_response
from datetime import datetime
import json
from reportlab.lib.pagesizes import letter, A4
from reportlab.platypus import SimpleDocTemplate, Paragraph, Spacer, Table, TableStyle
from reportlab.lib.styles import getSampleStyleSheet, ParagraphStyle
from reportlab.lib.units import inch
from reportlab.lib import colors
from reportlab.lib.enums import TA_CENTER, TA_LEFT
import io
import os
from models.report import Report, db

report_bp = Blueprint("report", __name__)

@report_bp.route("/reports/", methods=["POST"])
def create_report():
    try:
        data = request.get_json()
        
        # Validação dos campos obrigatórios
        required_fields = ["operator_name", "shift_type", "shift_date", "activities"]
        for field in required_fields:
            if not data.get(field):
                return jsonify({"error": f"Campo {field} é obrigatório"}), 400
        
        # Conversão da data
        try:
            shift_date = datetime.fromisoformat(data["shift_date"].replace("Z", "+00:00"))
        except ValueError:
            return jsonify({"error": "Formato de data inválido"}), 400
        
        # Criação do relatório
        report = Report(
            operator_name=data["operator_name"],
            shift_type=data["shift_type"],
            shift_date=shift_date,
            activities=data["activities"],
            occurrences=data.get("occurrences", ""),
            observations=data.get("observations", "")
        )
        
        db.session.add(report)
        db.session.commit()
        
        return jsonify(report.to_dict()), 201
        
    except Exception as e:
        db.session.rollback()
        return jsonify({"error": str(e)}), 500

@report_bp.route("/reports/", methods=["GET"])
def get_reports():
    try:
        page = request.args.get("page", 1, type=int)
        per_page = request.args.get("per_page", 10, type=int)
        
        reports = Report.query.order_by(Report.created_at.desc()).paginate(
            page=page, per_page=per_page, error_out=False
        )
        
        return jsonify({
            "reports": [report.to_dict() for report in reports.items],
            "total": reports.total,
            "pages": reports.pages,
            "current_page": page
        })
        
    except Exception as e:
        return jsonify({"error": str(e)}), 500

@report_bp.route("/reports/<int:report_id>", methods=["GET"])
def get_report(report_id):
    try:
        report = Report.query.get_or_404(report_id)
        return jsonify(report.to_dict())
        
    except Exception as e:
        return jsonify({"error": str(e)}), 500

@report_bp.route("/reports/<int:report_id>/pdf", methods=["GET"])
def generate_pdf(report_id):
    try:
        report = Report.query.get_or_404(report_id)
        
        # Criar PDF em memória
        buffer = io.BytesIO()
        doc = SimpleDocTemplate(buffer, pagesize=A4, topMargin=1*inch)
        
        # Estilos
        styles = getSampleStyleSheet()
        title_style = ParagraphStyle(
            "CustomTitle",
            parent=styles["Heading1"],
            fontSize=18,
            spaceAfter=30,
            alignment=TA_CENTER,
            textColor=colors.HexColor("#ff7a00")
        )
        
        heading_style = ParagraphStyle(
            "CustomHeading",
            parent=styles["Heading2"],
            fontSize=14,
            spaceAfter=12,
            textColor=colors.HexColor("#1a1a1a")
        )
        
        normal_style = ParagraphStyle(
            "CustomNormal",
            parent=styles["Normal"],
            fontSize=11,
            spaceAfter=12,
            alignment=TA_LEFT
        )
        
        # Conteúdo do PDF
        story = []
        
        # Título
        story.append(Paragraph("Usina Coruripe • AutoBo Turno Reporter", title_style))
        story.append(Spacer(1, 20))
        
        # Informações do relatório
        info_data = [
            ["Relatório ID:", str(report.id)],
            ["Operador:", report.operator_name],
            ["Tipo de Turno:", report.shift_type],
            ["Data/Hora do Turno:", report.shift_date.strftime("%d/%m/%Y %H:%M")],
            ["Data de Criação:", report.created_at.strftime("%d/%m/%Y %H:%M")]
        ]
        
        info_table = Table(info_data, colWidths=[2*inch, 4*inch])
        info_table.setStyle(TableStyle([
            ("BACKGROUND", (0, 0), (0, -1), colors.HexColor("#f6f6f6")),
            ("TEXTCOLOR", (0, 0), (-1, -1), colors.black),
            ("ALIGN", (0, 0), (-1, -1), "LEFT"),
            ("FONTNAME", (0, 0), (0, -1), "Helvetica-Bold"),
            ("FONTNAME", (1, 0), (1, -1), "Helvetica"),
            ("FONTSIZE", (0, 0), (-1, -1), 10),
            ("GRID", (0, 0), (-1, -1), 1, colors.black),
            ("VALIGN", (0, 0), (-1, -1), "TOP"),
        ]))
        
        story.append(info_table)
        story.append(Spacer(1, 20))
        
        # Atividades
        story.append(Paragraph("Atividades Realizadas", heading_style))
        story.append(Paragraph(report.activities, normal_style))
        story.append(Spacer(1, 15))
        
        # Ocorrências
        if report.occurrences:
            story.append(Paragraph("Ocorrências", heading_style))
            story.append(Paragraph(report.occurrences, normal_style))
            story.append(Spacer(1, 15))
        
        # Observações
        if report.observations:
            story.append(Paragraph("Observações", heading_style))
            story.append(Paragraph(report.observations, normal_style))
        
        # Construir PDF
        doc.build(story)
        
        # Preparar resposta
        buffer.seek(0)
        response = make_response(buffer.getvalue())
        response.headers["Content-Type"] = "application/pdf"
        response.headers["Content-Disposition"] = f"attachment; filename=relatorio_turno_{report.id}.pdf"
        
        return response
        
    except Exception as e:
        return jsonify({"error": str(e)}), 500

