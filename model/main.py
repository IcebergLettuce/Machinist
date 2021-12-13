import sys
import os
import pika
import random
import time
# Branch off and create search branch
# Pipeline build cntainer and submits statespace to the queue with the git commit tag (version)
# Each statespace configuration has its own sub-ID on the queue
# A deployment.yaml in the search folder is generated to trigger ArgoCD which pulls it
# MOdel gets deployed and works of queue => submits to performnnce topic

UUID = os.getenv('UUID')

def on_state(channel, method_frame, header_frame, body):
    print(method_frame.delivery_tag)
    print(f'Running Model with {body} parameters')

    for i in range(100):
        print(f'Iteration {i} of 100')
        time.sleep(0.3)

    channel.basic_ack(delivery_tag=method_frame.delivery_tag)
    channel.basic_publish(exchange='',routing_key='performance', body= json.dumps({UUID:"very good"}))
    print('published performance')

def main():

    print("start")
    connection = pika.BlockingConnection(pika.ConnectionParameters("pipr.io",port = 5672))
    channel = connection.channel()
    channel.queue_declare('performance',durable = True)
    channel.basic_consume(UUID, on_state)
    channel.start_consuming()
    
if __name__ == '__main__':
    main()