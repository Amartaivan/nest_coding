import math

if __name__ == "__main__":
    in_str = raw_input()
    x = float(in_str.split()[0])
    n = int(in_str.split()[1])
 
    result = 0
    current_number = 1
    for i in range(1, n + 1):
        current_number *= x
        print current_number
        result += math.sin(current_number)
    print round(result, 3)