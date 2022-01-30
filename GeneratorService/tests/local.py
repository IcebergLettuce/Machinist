import redis
r = redis.Redis(host='localhost', port=6379, db=0)
response = r.set('foo', 'bar')
assert response == True