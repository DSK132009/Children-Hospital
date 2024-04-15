// Keyboard Enum
export const _KEYBOARD_ = Object.freeze({
    'enter': 13,
    'arrow-u': 38,
    'arrow-r': 39,
    'arrow-d': 40,
    'arrow-l': 37,
    'esc': 27,
    'tab': 9,
    'space': 32
  });
  
  /**
   * Breakpoints are situatued such that
   * comparison would be "and down" --
   * 
   * if (window.innerWidth < _BREAKPOINTS_.sm) { ... }
   */
  export const _BREAKPOINTS_ = Object.freeze({
    'xs': 576,
    'sm': 767,
    'md': 991,
    'lg': 1199,
    'xl': 1399,
    'giant': 1799
  });