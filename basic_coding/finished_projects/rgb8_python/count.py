if __name__ == '__main__':
	input = map(int, raw_input().split())
	last = input[0]
	result = 2
	while last != input[result - 1]:
		last = input[result - 1]
		result += 1
	print result