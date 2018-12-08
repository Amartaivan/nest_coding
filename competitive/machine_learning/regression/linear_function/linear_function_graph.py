from sklearn import linear_model
import numpy

def predict(n, lin_reg):
	return lin_reg.predict(n)[0]

if __name__ == "__main__":
	in_x = numpy.array(map(float, raw_input().split())).reshape(-1, 1)
	in_y = numpy.array(map(float, raw_input().split()))
	
	lin_reg = linear_model.LinearRegression()
	lin_reg.fit(in_x, in_y)
	
	with open("graph.csv", "w") as csvfile:
		csvfile.write("Graph\n")
		for i in range(31):
			csvfile.write(str(i) + '\n')
			csvfile.write(str(predict(i, lin_reg)) + '\n')