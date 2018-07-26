if __name__ == '__main__':
	nums = map(int, raw_input().split())
	length = 0
	output = ""
	i = 0
	while nums[i] != 0:
		tmp = 0
		j = 1
		while j <= nums[i]:
			tmp *= 10
			tmp += nums[i + j]
			j += 1
		output += str(tmp) + ' '
		length += 1
		i += j
		
	output = str(length) + ' ' + output
	print output