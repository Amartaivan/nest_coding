from sklearn import linear_model
import numpy

def predict(n, lin_reg):
	return lin_reg.predict(n)[0]

if __name__ == "__main__":
	in_x = numpy.array(map(float, raw_input().split())).reshape(-1, 1)
	in_y = numpy.array(map(float, raw_input().split()))
	
	lin_reg = linear_model.LinearRegression()
	lin_reg.fit(in_x, in_y)
	
	print "If y = ax + b:"
	print "a =", lin_reg.coef_[0]
	print "b =", lin_reg.intercept_