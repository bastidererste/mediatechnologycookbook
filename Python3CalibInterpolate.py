import numpy as np

calPoints = [1000, 2000 , 3000, 4000, 5000, 6000]
data = [11, 22, 33, 44, 55, 66]

for v in range(0,7000):
    result = np.interp(v, calPoints, data)
    print(result)
