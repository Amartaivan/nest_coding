if __name__ == '__main__':
	output = ""
	n = int(raw_input())
	nums = map(int, raw_input().split())
	for i in range(0, n, 2):
		output += str(nums[i]) + ' '
	for i in range(1, n, 2):
		output += str(nums[i]) + ' '
	print output