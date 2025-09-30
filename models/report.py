from datetime import datetime
from .user import db

class Report(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    operator_name = db.Column(db.String(100), nullable=False)
    shift_type = db.Column(db.String(50), nullable=False)
    shift_date = db.Column(db.DateTime, nullable=False)
    activities = db.Column(db.Text, nullable=False)
    occurrences = db.Column(db.Text, nullable=True)
    observations = db.Column(db.Text, nullable=True)
    created_at = db.Column(db.DateTime, default=datetime.utcnow)

    def __repr__(self):
        return f'<Report {self.id} - {self.operator_name}>'

    def to_dict(self):
        return {
            'id': self.id,
            'operator_name': self.operator_name,
            'shift_type': self.shift_type,
            'shift_date': self.shift_date.isoformat() if self.shift_date else None,
            'activities': self.activities,
            'occurrences': self.occurrences,
            'observations': self.observations,
            'created_at': self.created_at.isoformat() if self.created_at else None
        }

