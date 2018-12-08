def combsort(array):
	gap = (len(array) * 10 // 13) if len(array) > 0 else 0 #gap factor = n * 10 / 13
	while gap:
		swapped = 0
		for i in range(len(array) - gap):
			if array[i + gap] < array[i]:
				array[i], array[i + gap] = array[i + gap], array[i]
				swapped = 1
		gap = (gap * 10 // 13) or swapped #(gap * 10 // 13) == 0 ? swapped : (gap * 10 // 13)
	return array
		
if __name__ == '__main__':
	a = map(int, raw_input().split())
	combsort(a)
	print a