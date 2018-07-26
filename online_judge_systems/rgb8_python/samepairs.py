if __name__ == '__main__':
	count = 0
	n = int(raw_input())
	nums = map(int, raw_input().split())
	for i in range(n):
		for j in range(i + 1, n):
			if nums[i] == nums[j]:
				count += 1
	print count