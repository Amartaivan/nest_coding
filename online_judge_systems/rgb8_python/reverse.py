if __name__ == '__main__':
	n = int(raw_input())
	nums = map(int, raw_input().split())
	nums = nums[::-1]
	output = ''
	for num in nums:
		output = output + str(num) + ' '
	print output