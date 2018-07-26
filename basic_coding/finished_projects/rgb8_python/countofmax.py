if __name__ == '__main__':
	n = int(raw_input())
	nums = map(int, raw_input().split())
	max_number = max(nums)
	count = 0
	for a in nums:
		if a == max_number:
			count += 1
	print count