export const usernameRegex = /^(?!.*\.\.)(?!.*\.$)[^\W][\w.]{0,29}$/im;
export const nameRegex = /^[a-zA-ZА-ЩЬЮЯҐЄІЇа-щьюяґєії]+$/m;
export const emailRegex = /^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$/m;