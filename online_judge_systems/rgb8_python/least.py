if __name__ == '__main__':
	n = int(raw_input())
	nums = map(int, raw_input().split())
	least = nums[0]
	for i in range(n):
		if least > nums[i]:
			least = nums[i]
	print least