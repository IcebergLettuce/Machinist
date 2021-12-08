import os
import json
import itertools

with open('statespace/statespace.json','r') as f:
    statespaces = json.load(f)

mutations = [dict(zip(statespaces, col)) for col in zip(*statespaces.values())]

with open('queue', 'w') as f:
    f.write(json.dumps(mutations[0]))