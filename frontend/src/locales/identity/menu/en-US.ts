export default {
  identity: {
    menu: {
      title: 'Menu Management',
      columns: {
        menuName: 'Menu Name',
        icon: 'Icon',
        orderNum: 'Order',
        perms: 'Permission',
        component: 'Component Path',
        status: 'Status',
        createTime: 'Create Time',
        action: 'Action'
      },
      form: {
        base: {
          parentMenu: {
            label: 'Parent Menu',
            placeholder: 'Please select parent menu',
            root: 'Root Menu'
          },
          name: {
            label: 'Menu Name',
            placeholder: 'Please input menu name'
          },
          transKey: {
            label: 'Translation Key',
            placeholder: 'Please input translation key',
            preview: 'Preview',
            notFound: 'Translation not found'
          },
          orderNum: {
            label: 'Display Order'
          }
        },
        display: {
          type: {
            label: 'Menu Type',
            directory: 'Directory',
            menu: 'Menu',
            button: 'Button'
          },
          icon: {
            label: 'Menu Icon',
            placeholder: 'Please input menu icon'
          },
          isFrame: {
            label: 'External Link',
            yes: 'Yes',
            no: 'No'
          },
          isCache: {
            label: 'Cache',
            yes: 'Yes',
            no: 'No'
          },
          visible: {
            label: 'Display Status',
            show: 'Show',
            hide: 'Hide'
          },
          status: {
            label: 'Menu Status',
            normal: 'Normal',
            disabled: 'Disabled'
          }
        },
        route: {
          path: {
            label: 'Route Path',
            placeholder: 'Please input route path'
          },
          component: {
            label: 'Component Path',
            placeholder: 'Please input component path'
          },
          query: {
            label: 'Route Parameters',
            placeholder: 'Please input route parameters'
          }
        },
        permission: {
          perms: {
            label: 'Permission',
            placeholder: 'Please input permission'
          }
        }
      },
      operation: {
        add: {
          title: 'Add Menu',
          success: 'Added successfully',
          failed: 'Add failed'
        },
        edit: {
          title: 'Edit Menu',
          success: 'Modified successfully',
          failed: 'Modify failed'
        },
        delete: {
          title: 'Delete Menu',
          confirm: 'Are you sure to delete this menu?',
          success: 'Deleted successfully',
          failed: 'Delete failed'
        }
      }
    }
  }
} 