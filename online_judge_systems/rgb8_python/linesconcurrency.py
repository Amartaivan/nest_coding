if __name__ == '__main__':
	n = int(raw_input())
	noconc = False
	x1, y1 = map(int, raw_input().split())
	for i in range(1, n):
		x2, y2 = map(int, raw_input().split())
		if y1 - x2 < 0:
			noconc = True
			break
		else:
			if x2 > x1:
				x1 = x2
			if y2 < y1:
				y1 = y2
	if noconc:
		print 0
	else:
		print y1 - x1