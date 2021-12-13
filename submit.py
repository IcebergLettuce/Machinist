import os
import json
import itertools
import pika
import threading
import sys


uuid = sys.argv[1]

connection = pika.BlockingConnection(pika.ConnectionParameters("pipr.io",port = 5672))
channel = connection.channel()
channel.queue_declare(queue=uuid, durable=True)

states = [{"lr":2.0,"beta":1.2},{"lr":1.2,"beta":0.2}]

for i, state in enumerate(states):
    channel.basic_publish(exchange='',routing_key=uuid, body=json.dumps(state))
    print(f'published state {i}')

print("Sent States")
connection.close()

pod = f'''
apiVersion: v1
kind: Pod
metadata:
  name: mlopsi
spec:
  containers:
  - name: mlopsi
    image: hirzeman/model:{uuid}
'''

with open('search/deployment.yaml', 'w') as f:
    f.write(pod)