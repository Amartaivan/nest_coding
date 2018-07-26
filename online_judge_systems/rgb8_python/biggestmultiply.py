if __name__ == '__main__':
    n = int(raw_input())
    nums = map(int, raw_input().split())

    mx = nums[0] * nums[1] * nums[2]
    max_i = 0
    max_j = 1
    max_k = 2
    for i in range(n - 2):
        for j in range(i + 1, n - 1):
            for k in range(j + 1, n):
                if nums[i] * nums[j] * nums[k] > mx:
                    mx = nums[i] * nums[j] * nums[k]
                    max_i = i
                    max_j = j
                    max_k = k
    output = str(nums[max_i]) + ' ' + str(nums[max_j]) + ' ' + str(nums[max_k])
    print output