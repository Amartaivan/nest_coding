if __name__ == '__main__':
	n = int(input())
	s = 0
	while n > 0:
		s += n % 10
		n /= 10
	print s