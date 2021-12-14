FROM python:3.9.7-slim
COPY requirements.txt requirements.txt
RUN pip3 install -r requirements.txt
# You can set VAR_A while building the image
# or leave it at the default
ARG VAR_A 5
# VAR_B gets the (overridden) value of VAR_A
ENV UUID $VAR_A
COPY . .
CMD ["python", "main.py"]