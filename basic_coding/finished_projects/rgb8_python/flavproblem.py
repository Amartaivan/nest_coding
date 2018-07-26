if __name__ == '__main__':
    n, k = map(int, raw_input().split())
    A = [0] * n
    ln = n

    i = k - 1
    while ln > 1:
        A[i % n] = 1

        j = 0
        i += 1
        while j < k:
            if A[i % n] != 1:
                j += 1
            i += 1

        i -= 1
        ln -= 1

    for i in range(n):
        if A[i] == 0:
            print i + 1
            break