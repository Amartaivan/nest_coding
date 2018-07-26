def gcd (n, m):
	n = abs(n) #Prevent errors
	m = abs(m) #Prevent errors
	while n > 0 and m > 0:
		if n > m:
			n %= m
		else:
			m %= n
	return n + m
	
if __name__ == '__main__':
	n, m = map(int, raw_input().split())
	print n * m / gcd(n, m)