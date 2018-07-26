if __name__ == '__main__':
	output = ""
	count = [0] * 10
	input = map(int, raw_input().split())
	for num in input:
		if num == 0:
			break;
		count[num] += 1
	for i in range(1, 10):
		output += str(count[i]) + ' '
	print output