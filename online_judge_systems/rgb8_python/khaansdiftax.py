if __name__ == '__main__':
	n = int(raw_input())
	V = map(int, raw_input().split())
	p = map(int, raw_input().split())
	maxtax = V[0] * p[0] / 100.0
	maxtax_i = 0
	for i in range(1, n):
		if V[i] * p[i] / 100.0 > maxtax:
			maxtax = V[i] * p[i] / 100.0
			maxtax_i = i
	print maxtax_i + 1