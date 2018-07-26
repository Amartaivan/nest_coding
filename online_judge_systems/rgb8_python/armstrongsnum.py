if __name__ == '__main__':
    n = int(raw_input())

    nums = []
    sum = 0
    result = n

    while n > 0:
        nums.append(n % 10)
        n /= 10

    for num in nums:
        sum += num ** len(nums)
    
    if result == sum:
        print "YES"
    else:
        print "NO"