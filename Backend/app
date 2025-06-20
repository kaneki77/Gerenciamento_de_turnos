from flask import Flask
from flask_sqlalchemy import SQLAlchemy
from flask_cors import CORS

db = SQLAlchemy()

def create_app():
    app = Flask(__name__)
    CORS(app)
    app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///shifts.db'
    app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False

    db.init_app(app)

    from .routes import bp
    app.register_blueprint(bp)

    return app


from . import db
from .models import Shift, Task

def init_db(app):
    with app.app_context():
        db.create_all()


from . import db

class Shift(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    date = db.Column(db.String(10))
    type = db.Column(db.String(1))
    hours = db.Column(db.String(20))
    supervisor = db.Column(db.String(100))
    team_members = db.Column(db.Text)
    summary = db.Column(db.Text)
    incidents = db.Column(db.Text)
    criticality = db.Column(db.String(10))
    suggestions = db.Column(db.Text)

class Task(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    shift_id = db.Column(db.Integer, db.ForeignKey('shift.id'))
    task_type = db.Column(db.String(20))
    start_time = db.Column(db.String(5))
    end_time = db.Column(db.String(5))
    duration = db.Column(db.String(10))
    description = db.Column(db.Text)
    requester = db.Column(db.String(100))
    responsible = db.Column(db.String(100))


from flask import Blueprint, request, jsonify
from . import db
from .models import Shift, Task

bp = Blueprint('main', __name__)

@bp.route('/shifts', methods=['POST'])
def create_shift():
    data = request.get_json()
    shift = Shift(**data)
    db.session.add(shift)
    db.session.commit()
    return jsonify({'message': 'Shift created', 'id': shift.id}), 201

@bp.route('/shifts/<int:id>/tasks', methods=['POST'])
def add_tasks(id):
    data = request.get_json()
    for task_data in data:
        task = Task(shift_id=id, **task_data)
        db.session.add(task)
    db.session.commit()
    return jsonify({'message': 'Tasks added'}), 201

@bp.route('/shifts', methods=['GET'])
def list_shifts():
    shifts = Shift.query.all()
    result = [{'id': s.id, 'date': s.date, 'type': s.type, 'supervisor': s.supervisor} for s in shifts]
    return jsonify(result)
