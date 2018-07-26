if __name__ == '__main__':
	result_i = 0
	result_j = 0
	n = int(raw_input())
	nums = map(int, raw_input().split())
	for i in range(n):
		for j in range(i + 1, n):
			if nums[i] == nums[j] and result_i == 0 and result_j == 0:
				result_i = i
				result_j = j
	if result_i != 0 or result_j != 0:
		print str(result_i + 1) + ' ' + str(result_j + 1)
	else:
		print "0 0"