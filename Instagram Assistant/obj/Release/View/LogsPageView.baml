   M S B A M L   `   `   `  ���� R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel   Instagram Assistant+   %Instagram_Assistant.View.LogsPageView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006      @d2http://schemas.microsoft.com/expression/blend/2008      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase      Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/    � 	Resources     UserMessage  BorderColor �� CornerRadius �� Orientation!  FontAwesome.WPF.FontAwesome  SecondColorBrush   Icon �� Padding  FontAwesome.WPF.Awesome   Content  Regular  MainFont � Triggers :� RoutedEvent �� TargetProperty	 t� From	
 t� To g� AccelerationRatio g� Duration t� EasingFunction0  *System.Windows.Media.Animation.ElasticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode   Oscillations   Springiness  	logGroups �� Source �� Path �� SortDescriptions+   %System.ComponentModel.SortDescription   PropertyName   	Direction��%7  &	        7  7  7  7  &	 �    �� .R��� 8��  $��60��$	��775��$ 3��._��� $ 
Horizontal=�$��0,5,0,5q�.+��� $ 
Horizontal=�$��	15,0,15,0q�.+�  $��20(�$��Center=�8��  �� *icon+�� $ Vertical=�$��Center=�$	��100��.+��� $��Center=�$��15(�
�������$ 0q����� *type+�� 
�������$ 	UserTimes=�$��Center=�$��10(����� *detailedtype+�� $ 
Horizontal=�$��	15,0,15,0q�.+��� 
�������$��15(�$��Center=����� *date+�� $ 
Horizontal=�.+��� $��Center=�$
��Wrap=�$	��540��
�������8�� 8�� ���� *	message+ :� $ Loaded��[��� .���� .��t� $ Margin�$	 
-100,0,0,0X�$
 0X�$	 0.9Q�$ 0:0:0.5D�   $ EaseOut=�$ 2��$ 7Q���  �� $ 
ActionList�   $
 date��$ 
Descending=�.���  � RowDefinitions �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� ��  � ColumnDefinitions �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� �� .+��� $��0��$��1��$��1��$��6��$
��LOGS��$��Center=�$��30(�
�������  TextBlockMainStyle#���  �� $��1��$��26��$��1��$��16��
������ 
������ #���   ����  �� Source# [ 