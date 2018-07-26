def sumofdivisors (n):
	sum = 0
	for a in range(1, n + 1):
		if n % a == 0:
			sum += a
	return sum

if __name__ == '__main__':
	n = int(input())
	result = 1
	for a in range(1, n + 1):
		if sumofdivisors(a) >= sumofdivisors(result):
			result = a
	print result