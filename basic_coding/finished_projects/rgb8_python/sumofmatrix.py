if __name__ == '__main__':
    n, m = map(int, raw_input().split())
    A = [map(int, raw_input().split()) for i in range(n)]
    
    sum = 0
    for i in range(n):
        for j in range(m):
            sum += A[i][j]
    print sum