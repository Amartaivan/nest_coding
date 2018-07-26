if __name__ == '__main__':
	count = 0
	n = int(raw_input())
	nums = map(int, raw_input().split())
	for i in range(n - 1):
		if nums[i] == nums[i + 1]:
			count += 1
	print count