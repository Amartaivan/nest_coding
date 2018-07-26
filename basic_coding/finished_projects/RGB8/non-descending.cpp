#include <iostream>

using namespace std;

int main(){
	
	int n, i, j, B[2000];
	cin >> n;
	long A[n];
	
	for (i = 0; i < 2000; i++)
		B[i] = 0;
	for (i = 0; i < n; i++){
		cin >> A[i];
		B[A[i] + 1000]++;
	}
	
	for (i = 0; i < 2000; i++)
		if (B[i] > 0)
			for (j = 0; j < B[i]; j++)
				cout << i - 1000 << ' ';
	return 0;
}
