def mergesort(array):
	if len(array) > 1:
		mid = len(array) / 2
		left_half = mergesort(array[:mid]) 
		right_half = mergesort(array[mid:len(array)])
		
		i = 0 
		j = 0
		while i < len(left_half) and j < len(right_half):
			if left_half[i] > right_half[j]:
				array[i + j] = right_half[j]
				j += 1
			else:
				array[i + j] = left_half[i]
				i += 1
		while i < len(left_half):
			array[i + j] = left_half[i]
			i += 1
		while j < len(right_half):
			array[i + j] = right_half[j]
			j += 1
	return array
	
if __name__ == '__main__':
	a = map(int, raw_input().split())
	a = mergesort(a)
	print a