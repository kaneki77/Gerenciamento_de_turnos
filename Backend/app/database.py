from . import db
from .models import Shift, Task

def init_db(app):
    with app.app_context():
        db.create_all()

