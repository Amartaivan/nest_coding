if __name__ == '__main__':
    n = int(raw_input())
    nums = map(int, raw_input().split())
    a = int(raw_input())
    del nums[a - 1]
    output = ""
    for num in nums:
        output += str(num) + ' '
    print output