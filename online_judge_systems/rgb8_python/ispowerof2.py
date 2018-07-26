if __name__ == '__main__':
	n = int(input())
	bits = 0
	while n > 0:
		if n & 1:
			bits += 1
		n = n >> 1
	if bits > 1:
		print "NO"
	else:
		print "YES"