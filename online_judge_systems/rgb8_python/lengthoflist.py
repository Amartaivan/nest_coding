if __name__ == '__main__':
	output = ""
	length = 0
	nums = map(int, raw_input().split())
	while nums[length] != 0:
		output += str(nums[length]) + ' '
		length += 1
	print length
	print output