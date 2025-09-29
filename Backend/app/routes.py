from flask import Blueprint, request, jsonify
from . import db
from .models import Shift, Task

bp = Blueprint("main", __name__)

@bp.route("/shifts", methods=["POST"])
def create_shift():
    data = request.get_json()
    shift = Shift(**data)
    db.session.add(shift)
    db.session.commit()
    return jsonify({"message": "Shift created", "id": shift.id}), 201

@bp.route("/shifts/<int:id>/tasks", methods=["POST"])
def add_tasks(id):
    data = request.get_json()
    for task_data in data:
        task = Task(shift_id=id, **task_data)
        db.session.add(task)
    db.session.commit()
    return jsonify({"message": "Tasks added"}), 201

@bp.route("/shifts", methods=["GET"])
def list_shifts():
    shifts = Shift.query.all()
    result = [{
        "id": s.id,
        "date": s.date,
        "type": s.type,
        "hours": s.hours,
        "supervisor": s.supervisor,
        "team_members": s.team_members,
        "summary": s.summary,
        "incidents": s.incidents,
        "criticality": s.criticality,
        "suggestions": s.suggestions
    } for s in shifts]
    return jsonify(result)

@bp.route("/shifts/<int:id>", methods=["GET"])
def get_shift(id):
    shift = Shift.query.get_or_404(id)
    tasks = Task.query.filter_by(shift_id=id).all()
    shift_data = {
        "id": shift.id,
        "date": shift.date,
        "type": shift.type,
        "hours": shift.hours,
        "supervisor": shift.supervisor,
        "team_members": shift.team_members,
        "summary": shift.summary,
        "incidents": shift.incidents,
        "criticality": shift.criticality,
        "suggestions": shift.suggestions,
        "tasks": [{
            "id": t.id,
            "task_type": t.task_type,
            "start_time": t.start_time,
            "end_time": t.end_time,
            "duration": t.duration,
            "description": t.description,
            "requester": t.requester,
            "responsible": t.responsible
        } for t in tasks]
    }
    return jsonify(shift_data)

