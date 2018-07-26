def sumofdigits (n):
	sum = 0
	while n > 0:
		sum += n % 10
		n /= 10
	return sum
	
if __name__ == '__main__':
	n, k = map(int, raw_input().split())
	count = 0
	for a in range(1, n + 1):
		if sumofdigits(a) % k == 0:
			count += 1
	print count