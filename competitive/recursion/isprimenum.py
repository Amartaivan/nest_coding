import math

def isPrime(n, div):
	if div > math.sqrt(n):
		return True
	if n % div == 0:
		return False
	return isPrime(n, div + 1)
	
if __name__ == '__main__':
	n = abs(input())
	if n < 4:
		print True
	else:
		print isPrime(n, 2)