#include <iostream>

using namespace std;

int main(){
    int n, m, min, tmp, i;
    cin >> n >> m >> min;
    for (i = 0; i < n * m - 1; i++){
        cin >> tmp;
        if (tmp < min)
            min = tmp;
    }
    cout << min << endl;
    return 0;
}