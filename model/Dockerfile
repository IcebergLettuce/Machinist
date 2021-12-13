FROM python:3.9.7-slim
COPY requirements.txt requirements.txt
RUN pip3 install -r requirements.txt
ENV UUID
COPY . .
CMD ["python", "main.py"]