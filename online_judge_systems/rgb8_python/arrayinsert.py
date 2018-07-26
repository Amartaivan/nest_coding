if __name__ == '__main__':
    n = int(raw_input())
    nums = map(int, raw_input().split())
    a, b = map(int, raw_input().split())
    nums.insert(a - 1, b)
    output = ""
    for num in nums:
        output += str(num) + ' '
    print output