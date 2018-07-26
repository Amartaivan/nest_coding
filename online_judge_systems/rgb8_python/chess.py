if __name__ == '__main__':
	x1, y1, x2, y2 = map(int, raw_input().split())
	if (x1 + y1) % 2 == (x2 + y2) % 2:
		print "YES"
	else:
		print "NO"