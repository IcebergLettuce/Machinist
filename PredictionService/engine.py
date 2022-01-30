import os
import json
import itertools
import pika
import threading
import sys

connection = pika.BlockingConnection(pika.ConnectionParameters("pipr.io",port = 5672))
channel = connection.channel()
channel.queue_declare(queue='statespace', durable=True)

states = [{"lr":2.0,"beta":1.2},{"lr":1.2,"beta":0.2}]

for i, state in enumerate(states):
    channel.basic_publish(exchange='',routing_key='statespace', body=json.dumps(state))
    print(f'published state {i}')


print("Sent States")
connection.close()
