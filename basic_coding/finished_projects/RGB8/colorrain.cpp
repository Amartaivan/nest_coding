#include <iostream>

using namespace std;

int main(){
	
	int n, i, j, result = 0;
	cin >> n;
	int A[n][n], B[n];
	
	for (i = 0; i < n; i++)
		for (j = 0; j < n; j++)
			cin >> A[i][j];
		
	for (i = 0; i < n; i++)
		cin >> B[i];
		
	for (i = 0; i < n; i++)
		for (j = i + 1; j < n; j++)
			if (A[i][j] == 1)
				if (B[i] != B[j])
					result++;

	cout << result << endl;
	return 0;
}
