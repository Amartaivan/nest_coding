if __name__ == '__main__':
	output = ""
	n = int(raw_input())
	nums = map(int, raw_input().split())
	for num in nums:
		if num % 2 == 1:
			output += str(num) + ' '
	for num in nums:
		if num % 2 == 0:
			output += str(num) + ' '
	print output