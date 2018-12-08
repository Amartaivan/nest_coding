#include <iostream>
#include <vector>

using namespace std;

int main(){
   
    int n, m, result = 0;
    cin >> n >> m;
    
    vector<int> array(n);
    for (int i = 0; i < n; i++)
        cin >> array[i];
    
    int sum = 0, st = 0, fin = 0;
    while (st < n){
        for (; fin < n && sum < m; fin++)
            sum += array[fin];

        if (sum >= m)
            result += n - fin + 1;
        sum -= array[st];
        st++;
    }

    cout << result << endl;
   return 0;
}
