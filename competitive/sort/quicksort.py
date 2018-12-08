def quicksort(array):
	less = []
	equal = []
	greater = []
	
	if len(array) > 1:
		pivot = (array[0] + array[len(array) / 2] + array[len(array) - 1]) / 3
		for element in array:
			if element < pivot:
				less.append(element)
			elif element == pivot:
				equal.append(element)
			else:
				greater.append(element)
		return quicksort(less) + equal + quicksort(greater)
	else:
		return array
		
if __name__ == '__main__':
	a = map(int, raw_input().split())
	a = quicksort(a)
	print a