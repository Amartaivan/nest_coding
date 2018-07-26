if __name__ == '__main__':
	n = int(raw_input())
	nums = map(int, raw_input().split())
	count = 0
	for i in range(1, n, 2):
		if nums[i] % 2 == 0:
			count += 1
	print count