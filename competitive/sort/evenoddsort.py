def evenoddsort(array):
	for i in range(len(array)):
		j = i % 2
		while j < len(array) - 1:
			if array[j] > array[j + 1]:
				array[j], array[j + 1] = array[j + 1], array[j]
			j += 2
	return array
	
if __name__ == '__main__':
	a = map(int, raw_input().split())
	evenoddsort(a)
	print a