#include <iostream>

using namespace std;

int main(){

    int n, i, result;
    cin >> n;

    float max_tax;
    float* V = new float[n];
    float* P = new float[n];
    float* R = new float[n];

    for (i = 0; i < n; i++)
        cin >> V[i];
    for (i = 0; i < n; i++)
    {
        cin >> P[i];
        R[i] = V[i] * P[i] / 100;
        if (i == 0 || R[i] > max_tax)
        {
            max_tax = R[i];
            result = i + 1;
        }
    }

    cout << result << endl;
    return 0;
}