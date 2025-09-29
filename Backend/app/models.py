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
    shift_id = db.Column(db.Integer, db.ForeignKey("shift.id"))
    task_type = db.Column(db.String(20))
    start_time = db.Column(db.String(5))
    end_time = db.Column(db.String(5))
    duration = db.Column(db.String(10))
    description = db.Column(db.Text)
    requester = db.Column(db.String(100))
    responsible = db.Column(db.String(100))

