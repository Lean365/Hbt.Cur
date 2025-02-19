export default {
  admin: {
    language: {
      title: 'Language Management',
      id: 'Language ID',
      code: {
        label: 'Language Code',
        placeholder: 'Please enter language code',
        validation: {
          required: 'Language code cannot be empty',
          length: 'Language code length must be between 2 and 20 characters'
        }
      },
      name: {
        label: 'Language Name',
        placeholder: 'Please enter language name',
        validation: {
          required: 'Language name cannot be empty',
          length: 'Language name length must be between 2 and 30 characters'
        }
      },
      icon: {
        label: 'Language Icon'
      }
    }
  }
} 